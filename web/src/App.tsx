/// <reference path="React.ts"/>
/// <reference path="WebPath.ts"/>

namespace hImageStorage {

    export class App {
        public webPath = "/images"
        public path = new hts.WebPath();
        public mainCtnr: JQuery;

        constructor() {
            return;
        }

        run() {
            $("body").append(<a class="w3-btn w3-black" href={this.webPath}>ImageStorage</a>);
            this.mainCtnr = $(<div class="w3-container"></div>);
            if (this.path.checkRouteMatch(this.webPath)) {
            }
        }
    }
    const app = new App();
    app.run();
}