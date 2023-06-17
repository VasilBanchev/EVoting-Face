async function initializeCameraInterop() {
    const video = document.getElementById('video');

    if (navigator.mediaDevices.getUserMedia) {
        try {
            const stream = await navigator.mediaDevices.getUserMedia({ video: true });
            video.srcObject = stream;
        } catch (error) {
            console.error('Error accessing the camera:', error);
        }
    } else {
        console.error('Browser does not support getUserMedia.');
    }
}
async function capturePhoto() {
    const canvas = document.getElementById('canvas');
    const video = document.getElementById('video');
    const context = canvas.getContext('2d');

    context.drawImage(video, 0, 0, canvas.width, canvas.height);
    canvas.style.display = 'block'; // Show the canvas element
    return canvas.toDataURL('image/jpeg');
}

async function stopCamera() {
    const video = document.getElementById('video');
    const stream = video.srcObject;
    const tracks = stream.getTracks();

    tracks.forEach(track => track.stop());
    video.srcObject = null;
}
function setElementStyle(elementId, styleProperty, styleValue) {
    const element = document.getElementById(elementId);
    element.style[styleProperty] = styleValue;
}

window.setElementStyle = setElementStyle;

window.initializeCameraInterop = initializeCameraInterop;
window.capturePhoto = capturePhoto;
window.stopCamera = stopCamera;