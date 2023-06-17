function displayImage(imageDataURL) {
    let image = new Image();
    image.src = imageDataURL;
    image.style.maxWidth = "100%";
    image.style.maxHeight = "100%";

    let w = window.open("", "_blank");
    w.document.write(image.outerHTML);
    w.document.close();
}
