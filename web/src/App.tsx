/// <reference path="React.ts"/>
/// <reference path="WebPath.ts"/>

namespace hImageStorage {

    export var webPath = "/images"

    export class App {
        public path = new hts.WebPath();

        constructor() {
            return;
        }

        run() {
            $("head").append($(<link rel="stylesheet" href={webPath + "/css-3rd/w3.css"}/>));
            if (this.path.checkRouteMatch(webPath)) {
            }
        }
    }
    const app = new App();
    app.run();
}