// this script is intended for use with the sphinx books theme for sphinx


// override ctrl + f command
document.addEventListener("keydown", function(e) {
    if (e.code === "KeyF" && e.ctrlKey) {
        e.preventDefault(); // cancel default behavior
        let navigation = document.getElementById("__navigation") // get the navigation element

        if (navigation.checked) { // if the navigation is not visible, show it, wait for a small delay and focus the search input
            document.getElementById("__navigation").checked = false;
            setTimeout(() => document.getElementById('search-input').focus(), 50);

            document.addEventListener("keydown", function(e) {
                if (e.ctrlKey) { // if the escape key is pressed, hide the navigation
                    document.getElementById("__navigation").checked = true;
                }
            },  { once: true }); // only listen to this event once since it will be redundant after the first time

        } else { // if the navigation is visible, just focus the search input
            document.getElementById('search-input').focus();
        }
    }
})