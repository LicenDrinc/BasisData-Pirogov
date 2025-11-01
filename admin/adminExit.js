if (localStorage.getItem("type_user") != 'admin')
{
	window.location.replace("http://localhost/PCLD/adminLogon/indexLogonAdmin.html");
}

document.getElementById("adminExit").addEventListener("submit", async function (e) {
	e.preventDefault();

	localStorage.setItem("id_user",  0);
	localStorage.setItem("type_user", '');
	
	window.location.replace("http://localhost/PCLD/");
});
