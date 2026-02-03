document.getElementById("workerLogin").addEventListener("submit", async function (e) {
	e.preventDefault();
	
	const formData = new FormData();
	
	formData.append('type','worker');
	formData.append('first_name',document.getElementById('first_name').value);
	formData.append('last_name',document.getElementById('last_name').value);
	formData.append('patrnymic',document.getElementById('patrnymic').value);
	formData.append('login',document.getElementById('login').value);
	formData.append('phone_number',document.getElementById('phone_number').value);
	formData.append('password',document.getElementById('password').value);
	formData.append('cop_password',document.getElementById('cop_password').value);
	
	const response1 = await fetch('loginTestServer.php', {
		method: 'POST',
		body: formData,
	});
	const result1 = await response1.text();
	
	if (result1[0] != '0')
	{
		document.getElementById('result').innerHTML = result1;
		return;
	}
	
	const response = await fetch('loginServer.php', {
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
