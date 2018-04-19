/// <reference path="WebPath.ts"/>

namespace hImageStorage {
    var UID = 0;
    const ElementIdPrefix = "hImageStorage_element_";
    export class Widget {
        element: JQuery = $(<div></div>);
        webPath: hts.WebPath;
        appPath: string;
        menuBar: JQuery;
        constructor(element: JQuery, proto: Widget) {
            this.element = element;
            if (proto != null) {
                this.webPath = proto.webPath;
                this.appPath = proto.appPath;
                this.menuBar = proto.menuBar;
            }
        }
        static getUID() {
            ++UID;
            return  ElementIdPrefix + UID;
        }
    }
}