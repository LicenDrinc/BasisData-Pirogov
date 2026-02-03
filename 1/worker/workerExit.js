if (localStorage.getItem("type_user") != 'worker')
{
	window.location.replace("http://localhost/PCLD/workerLogon/indexLogonWorker.html");
}

document.getElementById("workerExit").addEventListener("submit", async function (e) {
	e.preventDefault();

	localStorage.setItem("id_user",  0);
	localStorage.setItem("type_user", '');
	
	window.location.replace("http://localhost/PCLD/");
});
