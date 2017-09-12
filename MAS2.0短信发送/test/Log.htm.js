
//页面加载
$.page.pageLoad = function () {

    //    var form1 = new mini.Form("#logSeqNo");
    //    form1.setData(resultData);
    datagrid1_Load(1, 20);
};



//datagrid数据加载
function datagrid1_Load(pageIndex, pageSize) {
    //如果没传入页数
    if (!fw.fwObject.FWObjectHelper.hasValue(pageIndex)) {
        //将页数设置为datagrid的页数
        pageIndex = $.page.idM.datagrid1.pageIndex;
    };
    //如果没传入分页大小
    if (!fw.fwObject.FWObjectHelper.hasValue(pageSize)) {
        //将分页大小设置为datagrid的分页大小
        pageSize = $.page.idM.datagrid1.pageSize;
    };
    //排序字段
    var sortFieldList = null;
    //如果datagrid设置有排序字段
    if (fw.fwObject.FWObjectHelper.hasValue($.page.idM.datagrid1.sortField)) {
        //将排序字段设置为datagrid的排序字段
        sortFieldList = [{
            fieldName: $.page.idM.datagrid1.getSortField()
            , sortType: fw.fwData.FWSortType[$.page.idM.datagrid1.getSortOrder()]
        }];
    };
    //开启datagrid数据加载锁屏
    $.page.idM.datagrid1.loading();
    //加载数据
    //设置datagrid数据
    $.page.idM.datagrid1.set({
        pageIndex: pageIndex
        , pageSize: pageSize
        , totalCount: resultData.data.length
        , data: resultData.data
    });
    if (fw.fwObject.FWObjectHelper.hasValue($.page.params.onSelectionChangedCallback)) {
        if ($.page.idM.datagrid1.data.length > 0) {
            if (lastSelectedRowIndex > $.page.idM.datagrid1.data.length - 1) {
                lastSelectedRowIndex = $.page.idM.datagrid1.data.length - 1;
            } else if (lastSelectedRowIndex < 0) {
                lastSelectedRowIndex = 0;
            };
            $.page.idM.datagrid1.select($.page.idM.datagrid1.getRow(lastSelectedRowIndex));
        } else {
            datagrid1_SelectionChanged({ selected: {} });
        };
    };
};


var resultData = { "useTime": 0.0, "status": "Success", "data": [{ "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "1", "operDate": 1420041600000, "userName": "11", "ipAddress": "1", "operParameter": "1", "operResult": "1", "exception": "1", "operStatus": 1, "modelName": "1", "methodName": "1", "clock": null, "writeBln": true }, { "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "2", "operDate": 1420041600000, "userName": "2", "ipAddress": "2", "operParameter": "2", "operResult": "2", "exception": "2", "operStatus": 2, "modelName": "2", "methodName": "2", "clock": null, "writeBln": true }, { "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "3", "operDate": 1420041600000, "userName": "3", "ipAddress": "3", "operParameter": "3", "operResult": "3", "exception": "3", "operStatus": 3, "modelName": "3", "methodName": "3", "clock": null, "writeBln": true }, { "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "4", "operDate": 1420041600000, "userName": "4", "ipAddress": "4", "operParameter": "4", "operResult": "4", "exception": "4", "operStatus": 4, "modelName": "4", "methodName": "4", "clock": null, "writeBln": true }, { "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "5", "operDate": 1420041600000, "userName": "5", "ipAddress": "5", "operParameter": "5", "operResult": "5", "exception": "5", "operStatus": 5, "modelName": "5", "methodName": "5", "clock": null, "writeBln": true }, { "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "6", "operDate": 1420041600000, "userName": "6", "ipAddress": "6", "operParameter": "6", "operResult": "6", "exception": "6", "operStatus": 6, "modelName": "6", "methodName": "6", "clock": null, "writeBln": true }, { "id": null, "version": 0, "createTime": null, "page": { "currentPage": 0, "numPerPage": 0, "totalCount": 0, "recordList": null, "pageCount": 0, "beginPageIndex": 0, "endPageIndex": 0, "countResultMap": null }, "logSeqNo": null, "ticket": "7", "operDate": 1420041600000, "userName": "7", "ipAddress": "7", "operParameter": "7", "operResult": "7", "exception": "7", "operStatus": 7, "modelName": "7", "methodName": "7", "clock": null, "writeBln": true}], "sysInfoList": [], "bizInfoList": [], "sqlInfoList": [] };
