/// <reference path="React.ts"/>
/// <reference path="WebPath.ts"/>
/// <reference path="Gallery.tsx"/>
/// <reference path="Hamburger.tsx"/>

namespace hImageStorage {

    export class App {
        public appRootPath = "/images"
        public webPath = new hts.WebPath();
        public mainCtnr: JQuery;
        public menuBar: JQuery;
        public hamburger: JQuery;
        public appMenu: JQuery;
        public fhb: HTMLInputElement;

        constructor() {
            console.log("webPath is '" + this.webPath.path + "'");
        }

        run() {
            this.mainCtnr = $("#main");
            this.menuBar = $(
                <div class="menuBar" style="margin-bottom:2px">
                    {this.hamburger = this.createHamburger()}
                    <a class="w3-btn w3-black h-image-storage-main-button" href={this.appRootPath}>ImageStorage</a>
                </div>
            );
            this.mainCtnr.append(this.menuBar);
            this.mainCtnr.append(
                <div style="position: relative">
                    {this.appMenu = this.createAppMenu()}
                </div>
            );
            this.hamburger.on("click", () => this.appMenu.toggle());
            if (this.webPath.checkRouteMatch(this.appRootPath)) {
                this.runRoot();
            }
            this.fhb.onclick = () => console.log("!");
        }

        private createHamburger() {
            const hamburger = $(<div class="w3-btn w3-black hamburger"> </div>);
            new Hamburger(hamburger);
            return hamburger;
        }

        private createAppMenu() {
            const fitHeightBox: HTMLInputElement = <input type="checkbox" name="fitHeightBox"/>;
            //fitHeightBox.onclick = () => this.receiveFitHeightBoxChange(fitHeightBox.value);
            const appMenu = $(
                <div class="appMenu" style="position: absolute; display: none">
                    {fitHeightBox} fit height
                </div>
            );
            this.fhb = fitHeightBox;
            return appMenu;
        }

        private receiveFitHeightBoxChange(value: boolean) {
            console.log(value);
        }

        private initWidget(w: Widget) {
            w.webPath = this.webPath;
            w.appPath = this.appRootPath;
            w.menuBar = this.menuBar;
            w.init();
        }

        private runRoot() {
            const w = new Gallery();
            this.initWidget(w);
            this.mainCtnr.append(w.element);
        }
    }
    const app = new App();
    app.run();
}