/// <reference path="WebPath.ts"/>

namespace hImageStorage {
    export class Widget {
        element: JQuery = $(<div></div>);
        webPath: hts.WebPath;
        appPath: string;
        menuBar: JQuery;
        init() {
        }
    }
}