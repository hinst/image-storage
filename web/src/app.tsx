/// <reference path="React.ts"/>
/// <reference path="WebPath.ts"/>

namespace hImageStorage {

    export var webPath = "images"

    export class App {
        public path = new hts.WebPath();

        constructor() {
            return;
        }

        run() {
            if (this.path.checkRouteMatch(webPath)) {
            }
        }
    }
    const app = new App();
    app.run();
}