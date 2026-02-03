if (localStorage.getItem("type_user") == 'client')
{
	window.location.replace("http://localhost/PCLD/client/indexClient.html");
}

document.getElementById("clientLogon").addEventListener("submit", async function (e) {
	e.preventDefault();
	
	const formData = new FormData();
	
	formData.append('type','client');
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
		localStorage.setItem("type_user", 'client');
		document.getElementById('result').innerHTML = '';
		
		window.location.replace("http://localhost/PCLD/client/indexClient.html");
	}
	else
		document.getElementById('result').innerHTML = result;
});
