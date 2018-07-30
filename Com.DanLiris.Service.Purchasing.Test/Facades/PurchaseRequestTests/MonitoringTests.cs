﻿using Com.DanLiris.Service.Purchasing.Lib.Facades;
using Com.DanLiris.Service.Purchasing.Lib.Models.PurchaseRequestModel;
using Com.DanLiris.Service.Purchasing.Lib.Services;
using Com.DanLiris.Service.Purchasing.Test.DataUtils.PurchaseRequestDataUtils;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.DanLiris.Service.Purchasing.Test.Facades.PurchaseRequestTests
{
    [Collection("ServiceProviderFixture Collection")]
    public class MonitoringTests
    {
        private IServiceProvider ServiceProvider { get; set; }

        public MonitoringTests(ServiceProviderFixture fixture)
        {
            ServiceProvider = fixture.ServiceProvider;

            IdentityService identityService = (IdentityService)ServiceProvider.GetService(typeof(IdentityService));
            identityService.Username = "Unit Test";
        }

        private PurchaseRequestDataUtil DataUtil
        {
            get { return (PurchaseRequestDataUtil)ServiceProvider.GetService(typeof(PurchaseRequestDataUtil)); }
        }

        private PurchaseRequestFacade Facade
        {
            get { return (PurchaseRequestFacade)ServiceProvider.GetService(typeof(PurchaseRequestFacade)); }
        }

        

        public async void Should_Success_Get_Report_Data()
        {
            PurchaseRequest model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GetReport(model.No,model.UnitId,model.CategoryId,model.BudgetId,"","",null,null, 1,25,"{}",7);
            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Should_Success_Get_Report_Data_Null_Parameter()
        {
            PurchaseRequest model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GetReport("", null,null, null, "", "", null, null, 1, 25, "{}", 7);
            Assert.NotEqual(Response.Item2, 0);
        }

        [Fact]
        public async void Should_Success_Get_Report_Data_Excel()
        {
            PurchaseRequest model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GenerateExcel(model.No, model.UnitId, model.CategoryId, model.BudgetId, "", "", null, null, 7);
            Assert.IsType(typeof(System.IO.MemoryStream), Response);
        }

        [Fact]
        public async void Should_Success_Get_Report_Data_Excel_Null_Parameter()
        {
            PurchaseRequest model = await DataUtil.GetTestData("Unit test");
            var Response = Facade.GenerateExcel("", "", "", "", "", "", null, null, 7);
            Assert.IsType(typeof(System.IO.MemoryStream), Response);
        }
    }
}