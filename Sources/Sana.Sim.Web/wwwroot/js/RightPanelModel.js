var RightPanelViewModel = function() {
    var self = this;

    self.selectedTab = ko.observable(1);

    self.showTab = function (data) {
        self.selectedTab(data);
    };
};

ko.applyBindings(RightPanelViewModel, $('.side-container')[0]);