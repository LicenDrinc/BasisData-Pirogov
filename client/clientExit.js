if (localStorage.getItem("type_user") != 'client')
{
	window.location.replace("http://localhost/PCLD/clientLogon/indexLogonClient.html");
}

document.getElementById("clientExit").addEventListener("submit", async function (e) {
	e.preventDefault();

	localStorage.setItem("id_user",  0);
	localStorage.setItem("type_user", '');
	
	window.location.replace("http://localhost/PCLD/");
});
