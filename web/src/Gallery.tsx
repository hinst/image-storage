/// <reference path="Widget"/>

namespace hImageStorage {
    export class Gallery extends Widget {
        imageUI: HTMLElement;
        list: string[];

        _fitHeight: boolean;
        set fitHeight(fit: boolean) {
            this._fitHeight = fit;
            this.updateImageHeight();
        }

        init() {
            this.menuBar.append(<button class="w3-btn w3-black">&lt;</button>);
            this.menuBar.append(<button class="w3-btn w3-black">&gt;</button>);
            this.element.append(
                <div>
                    <hr style="margin-top: 0px; margin-bottom: 0px;"/>
                    {this.imageUI = <img alt="gallery image" class="gallery-image"/>}
                </div>
            );
            this.loadList();
            $(window).on("resize", () => this.updateImageHeight());
        }

        loadList() {
            $.ajax({
                url: this.appPath + "/GetImages",
                success: $.proxy(this.receiveList, this)
            });
        }

        receiveList(data: string[]) {
            this.list = data;
            if (!this.imageUI.getAttribute("src") && this.list.length > 0) {
                this.loadImage(this.list[0]);
            }
        }

        loadImage(id: string) {
            $.ajax({
                url: this.appPath + "/GetImage?id=" + encodeURIComponent(id),
                success: $.proxy(this.receiveImage, this),
            });
            this.imageUI.setAttribute("src", this.appPath + "/GetImageData?id=" + encodeURIComponent(id));
        }

        receiveImage(imageObject: WebImage) {
        }

        updateImageHeight() {
            $(this.imageUI).css("max-height", this._fitHeight ? window.innerHeight : "");
        }
    }
}