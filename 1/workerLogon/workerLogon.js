if (localStorage.getItem("type_user") == 'worker')
{
	window.location.replace("http://localhost/PCLD/worker/indexWorker.html");
}

document.getElementById("workerLogon").addEventListener("submit", async function (e) {
	e.preventDefault();
	
	const formData = new FormData();
	
	formData.append('type','worker');
	formData.append('login',document.getElementById('login').value);
	formData.append('password',document.getElementById('password').value);
	
	const response = await fetch('logonServer.php', {
		method: 'POST',
		body: formData,
	});
	
	const result = await response.text();
	
	if (result[3] == '=')
	{
		localStorage.setItem("id_user",  parseInt(result.slice(5,-1)));
		localStorage.setItem("type_user", 'worker');
		document.getElementById('result').innerHTML = '';
		
		window.location.replace("http://localhost/PCLD/worker/indexWorker.html");
	}
	else
		document.getElementById('result').innerHTML = result;
});
