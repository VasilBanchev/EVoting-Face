window.readUploadedFileAsArrayBuffer = (inputFile) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onerror = () => {
            reader.abort();
            reject(new Error("Error reading file."));
        };
        reader.onload = () => {
            resolve(reader.result);
        };
        reader.readAsArrayBuffer(inputFile.files[0]);
    });
};
