window.scrollToBottom = () => {
    setTimeout(() => {
            window.scrollTo(0, document.body.scrollHeight);
        },
        1);
}