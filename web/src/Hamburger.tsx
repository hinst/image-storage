namespace hImageStorage {
    export class Hamburger {
        constructor(element: JQuery) {
            for (var i = 0; i < 3; ++i) {
                var sausage = <div class="hamburgerSausage"/>;
                element.append(sausage);
            }
        }
    }
}