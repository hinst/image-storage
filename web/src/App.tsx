/// <reference path="React.ts"/>
/// <reference path="WebPath.ts"/>
/// <reference path="Gallery.tsx"/>
/// <reference path="Hamburger.tsx"/>

namespace hImageStorage {

    export class App {
        constructor() {
            console.log("webPath is '" + this.webPath.path + "'");
        }

        public appRootPath = "/images"
        public webPath = new hts.WebPath();
        mainCtnr: JQuery;
        menuBar: JQuery;
        hamburger: JQuery;
        appMenu: JQuery;
        fitHeightBoxId: string;
        gallery: Gallery;
        get fitHeightBox(): HTMLInputElement {
            return document.getElementById(this.fitHeightBoxId) as HTMLInputElement;
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
            this.fitHeightBox.onchange = () => this.receiveFitHeightBoxChange(this.fitHeightBox.checked);
        }

        private createHamburger() {
            const hamburger = $(<div class="w3-btn w3-black hamburger"> </div>);
            new Hamburger(hamburger);
            return hamburger;
        }

        private createAppMenu() {
            const fitHeightBox: HTMLInputElement = <input type="checkbox" name="fitHeightBox"/>;
            const appMenu = $(
                <div class="appMenu" style="position: absolute; display: none">
                    {fitHeightBox} fit height
                </div>
            );
            this.fitHeightBoxId = fitHeightBox.id;
            return appMenu;
        }

        private receiveFitHeightBoxChange(value: boolean) {
            if (this.gallery != null)
                this.gallery.fitHeight = value;
        }

        private createProtoWidget(): Widget {
            const w = new Widget(null, null);
            w.webPath = this.webPath;
            w.appPath = this.appRootPath;
            w.menuBar = this.menuBar;
            return w;
        }

        private runRoot() {
            const div = $(<div></div>);
            this.mainCtnr.append(div);
            const w = new Gallery(div, this.createProtoWidget());
            this.gallery = w;
        }

    }
    const app = new App();
    app.run();
}