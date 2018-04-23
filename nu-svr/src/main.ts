require('source-map-support').install();
import {App} from "./App"

console.log("STARTING...");

const app = new App();
app.run();

console.log("EXITING...");

