/// <reference path="React.ts"/>
/// <reference path="WebPath.ts"/>
/// <reference path="Gallery.tsx"/>

namespace hImageStorage {

    export class App {
        public appRootPath = "/images"
        public webPath = new hts.WebPath();
        public mainCtnr: JQuery;

        constructor() {
            console.log("webPath is '" + this.webPath.path + "'");
        }

        run() {
            this.mainCtnr = $("#main");;
            this.mainCtnr.append(<a class="w3-btn w3-black h-image-storage-main-button" href={this.appRootPath}>ImageStorage</a>);
            this.mainCtnr.append(<div style="height: 2px"></div>);
            if (this.webPath.checkRouteMatch(this.appRootPath)) {
                this.runRoot();
            }
        }

        initWidget(w: Widget) {
            w.webPath = this.webPath;
            w.appPath = this.appRootPath;
            w.init();
        }

        runRoot() {
            var w = new Gallery();
            this.initWidget(w);
            this.mainCtnr.append(w.element);
        }
    }
    const app = new App();
    app.run();
}