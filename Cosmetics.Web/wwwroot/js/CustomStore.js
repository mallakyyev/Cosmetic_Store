var getCategories = new DevExpress.data.AspNet.createStore({
    key: "id",
    loadUrl: "/api/CategoryAPI",
    onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
    }
});

var getManufactures = new DevExpress.data.AspNet.createStore({
    key: "id",
    loadUrl: "/api/ManufactureAPI",
    onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
    }
});

var getPublishCountries = new DevExpress.data.AspNet.createStore({
    key: "id",
    loadUrl: "/api/CountryAPI/GetPublishCountries",
    onBeforeSend: function (method, ajaxOptions) {
        ajaxOptions.xhrFields = { withCredentials: true };
    }
});