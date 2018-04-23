import * as path from "path"
import * as express from "express"
import * as Settings from "./Settings"
import * as fs from "fs"
import {Log} from "./Log"

const log = new Log(__filename);
const expressApp = express();

export class App {
    run() {

        this.prepareIndexPage();
        this.bindStaticFolder(Settings.WebPath, Settings.AppWwwBinDir);
        expressApp.listen(Settings.Port, () => {
            console.log("Now listening on port " + Settings.Port);
        });
    }

    bindStaticFolder(webPath: string, localPath: string) {
        localPath = path.normalize(localPath);
        log.info(webPath + " -> " + localPath);
        expressApp.use(webPath, express.static(localPath));
    }

    prepareIndexPage() {
        const text = fs.readFileSync(Settings.AppDir + "/../web/src-html/index.html").toString();
        const processedText = text.replace("{webPath}", Settings.WebPath);
        const outputFilePath = Settings.AppWwwBinDir + "/index.html";
        fs.writeFileSync(outputFilePath, processedText);
    }
}
