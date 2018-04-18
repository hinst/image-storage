/// <reference path="WebPath.ts"/>

namespace hImageStorage {
    var UID = 0;
    const ElementIdPrefix = "hImageStorage_element_";
    export class Widget {
        element: JQuery = $(<div></div>);
        webPath: hts.WebPath;
        appPath: string;
        menuBar: JQuery;
        init() {
        }
        static getUID() {
            ++UID;
            return  ElementIdPrefix + UID;
        }
    }
}