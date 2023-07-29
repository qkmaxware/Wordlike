function clipboard(text) {
    if (navigator.clipboard) {
        navigator.clipboard.writeText(text);
        alert("Copied to clipboard");
    }
}