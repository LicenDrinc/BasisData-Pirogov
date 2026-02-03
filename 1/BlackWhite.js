let test = localStorage.getItem("globolColor");

let link = document.getElementById("globolColor");
if (test == "true")
    link.setAttribute('href', "Black.css");
else if (test == "false")
    link.setAttribute('href', "White.css");
else localStorage.setItem("globolColor", true);

document.getElementById("settingsGlobolColor").addEventListener("click", () => {
    if (test == "true") localStorage.setItem("globolColor", false);
    else localStorage.setItem("globolColor", true);
});
