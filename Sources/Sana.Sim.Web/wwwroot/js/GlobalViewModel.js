var GlobalViewModel = function () {
    var self = this,
        pages = {
            paybackPage: '#PaybackCalculator'
        };

    //RIGHT PANEL TABS START
    self.selectedTab = ko.observable(1);
    self.showTab = function (data) {
        self.selectedTab(data);
    };
    //RIGHT PANEL TABS END

    //RIGHT PANEL TABS START
    self.selectedHeaderContent = ko.observable(1);
    self.showHeaderContent = function (data) {
        self.selectedHeaderContent(data);
    };
    //RIGHT PANEL TABS END

    if ($(pages.paybackPage).length) {
        self.totalSales = ko.observable(1000000);
        self.expectedOnlineSales = ko.observable(50);
        self.averageOrderValue = ko.observable(280);
        self.averageMinsPerOrder = ko.observable(15);
        self.averageMinsCheckOrder = ko.observable(2);
        self.yearlyOrderCost = ko.observable(30000);
        self.preEcommerceErrorRate = ko.observable(5);
        self.postEcommerceErrorRate = ko.observable(0.5);
        self.costPerError = ko.observable(50);
        self.requestPerDayService = ko.observable(15);
        self.averageMinsCustomerService = ko.observable(5);
        self.percentageSelfServedSanaRequests = ko.observable(50);
        self.yearlyCustomerServiceCost = ko.observable(50000);
        self.yearlyLicenseFee = ko.observable(20000);
        self.yearlyMaintenance = ko.observable(4000);
        self.percentageOrdersSalesForce = ko.observable(10);
        self.averageMinsOrderPreSana = ko.observable(15);
        self.averageMinsOrderPostSana = ko.observable(2);
        self.yearlySalesEmployeeCost = ko.observable(30000);
        self.expectedIncreaseTurnover247 = ko.observable(2);
        self.expectedIncreaseTurnoverUpselling = ko.observable(2);
        self.expectedIncreaseTurnoverPromo = ko.observable(2);
        self.leadToOrderTime = ko.observable(5);
        self.licenseFee = ko.observable(17000);
        self.firstYearMaintenance = ko.observable(2720);
        self.implementation = ko.observable(7000);
        self.hosting = ko.observable(2000);

        self.onlineOrderPerYear = ko.computed(function(){
            return ((self.totalSales() * (self.expectedOnlineSales() / 100)) / self.averageOrderValue()).toFixed();
        }, self);
        self.totalMinsSaved = ko.computed(function () {
            return self.averageMinsPerOrder() * self.onlineOrderPerYear();
        }, self);
        self.fteSavings = ko.computed(function(){
            return Math.round((self.totalMinsSaved() / 60 / 1776) * 100) / 100;
        });
        self.yearlySavingsOrderProcess = ko.computed(function(){
            return (self.fteSavings() * self.yearlyOrderCost()).toFixed();
        });
        self.yearlySavingsOrderErrors = ko.computed(function () {
            return (self.onlineOrderPerYear() * ((self.preEcommerceErrorRate() / 100) - (self.postEcommerceErrorRate() / 100)) * self.costPerError()).toFixed();
        });
        self.totalMinsSavedService = ko.computed(function () {
            return self.requestPerDayService() * (self.percentageSelfServedSanaRequests() / 100) * self.averageMinsCustomerService() * 222;
        });
        self.fteSavingsService = ko.computed(function () {
            return (self.totalMinsSavedService() / 60 / 1776).toFixed(2);
        });
        self.yearlySavingsCustomerService = ko.computed(function () {
            return (self.yearlyCustomerServiceCost() * self.fteSavingsService()).toFixed();
        });
        self.yearlySavingsReplaceSystems = ko.computed(function () {
            return parseFloat(self.yearlyLicenseFee()) + parseFloat(self.yearlyMaintenance());
        });
        self.timeSavedPerOrderAutomation = ko.computed(function () {
            return self.averageMinsOrderPreSana() - self.averageMinsOrderPostSana();
        });
        self.totalMinsSavedAutomation = ko.computed(function () {
            return self.timeSavedPerOrderAutomation() * (self.percentageOrdersSalesForce() / 100) * self.onlineOrderPerYear();
        });
        self.fteSavingsAutomation = ko.computed(function () {
            return Math.round(self.totalMinsSavedAutomation() / 60 / 1776 * 10000) / 10000;
        });
        self.yearlySavingsCustomerServiceAutomation = ko.computed(function () {
            return (self.fteSavingsAutomation() * self.yearlySalesEmployeeCost()).toFixed();
        });
        self.totalOnlineSales = ko.computed(function () {
            return self.totalSales() * (self.expectedOnlineSales() / 100);
        });
        self.revenueIncrease247 = ko.computed(function () {
            return totalOnlineSales() * (self.expectedIncreaseTurnover247() / 100);
        });
        self.revenueIncreaseUpselling = ko.computed(function () {
            return totalOnlineSales() * (self.expectedIncreaseTurnoverUpselling() / 100);
        });
        self.revenueIncreasePromo = ko.computed(function () {
            return totalOnlineSales() * (self.expectedIncreaseTurnoverPromo() / 100);
        });
        self.savedTime = ko.computed(function () {
            return self.totalMinsSavedAutomation() / 60 / 8 / 5;
        });
        self.extraOrders = ko.computed(function () {
            return self.savedTime() / self.leadToOrderTime();
        });
        self.revenueIncreaseAutomation = ko.computed(function () {
            return Math.round(self.extraOrders() * self.averageOrderValue()) * 100 / 100;
        });
        self.savingsTotal = ko.computed(function () {
            return parseFloat(self.yearlySavingsOrderProcess()) + parseFloat(self.yearlySavingsOrderErrors()) + parseFloat(self.yearlySavingsCustomerService()) +
                   parseFloat(self.yearlySavingsReplaceSystems()) + parseFloat(self.yearlySavingsReplaceSystems()) + parseFloat(self.yearlySavingsCustomerServiceAutomation());
        });
        self.revenueIncreaseTotal = ko.computed(function () {
            return self.revenueIncrease247() + self.revenueIncreaseUpselling() +
                   self.revenueIncreasePromo() + self.revenueIncreaseAutomation();
        });
        self.investmentsTotal = ko.computed(function () {
            return parseFloat(self.licenseFee()) + parseFloat(self.firstYearMaintenance()) +
                   parseFloat(self.implementation()) + parseFloat(self.hosting());
        });
        self.overall = ko.computed(function () {
            return (self.investmentsTotal() / (self.revenueIncreaseTotal() + self.savingsTotal()) * 365).toFixed();
        });
        self.overallVisible = true;
    }
    else {
        self.overall = null;
        self.overallVisible = false;
    }
};

ko.applyBindings(GlobalViewModel);