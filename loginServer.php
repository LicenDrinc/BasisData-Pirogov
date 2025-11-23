<?php
function printExit($d = '')
{
	print $d; exit;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$type = $_POST['type'];
	$Fname = $_POST['first_name'];
	$Lname = $_POST['last_name'];
	$Pname = $_POST['patrnymic'];
	$phone = $_POST['phone_number'];
	$login = $_POST['login'];
	$pas = $_POST['password'];
	$cop_pas = $_POST['cop_password'];
	
	if ($Fname == '') printExit("Имя не указан");
	if ($Lname == '') printExit("Фамилия не указан");
	if ($login == '') printExit("Логин не указан");
	if ($phone == '') printExit("телефон не указан");
	if ($pas == '') printExit("Пароль не указан");
	if ($cop_pas == '') printExit("Повторите пароль");
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$query = 'SELECT * FROM human GROUP BY id HAVING name = \''.($Pname == '' ? ($Fname.' '.$Lname) : ($Fname.' '.$Lname.' '.$Pname)).'\' AND phone_number = \''.$phone.'\'';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$id = 0;
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	if ($id != 0)
		break;
	$i = 0;
	foreach ($line as $col_value)
	{
		if ($col_value != null && $i == 0)
		{
			$id = $col_value;
			break;
		}
		$i++;
	}
}

if ($id == 0)
{
	$query = 'INSERT INTO human (name, phone_number) VALUES (\''.($Pname == '' ? ($Fname.' '.$Lname) : ($Fname.' '.$Lname.' '.$Pname)).'\', \''.$phone.'\');';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	
	$query = 'SELECT * FROM human GROUP BY id HAVING name = \''.($Pname == '' ? ($Fname.' '.$Lname) : ($Fname.' '.$Lname.' '.$Pname)).'\' AND phone_number = \''.$phone.'\'';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	
	while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
	{
		if ($id != 0)
			break;
		$i = 0;
		foreach ($line as $col_value)
		{
			if ($col_value != null && $i == 0)
			{
				$id = $col_value;
				break;
			}
			$i++;
		}
	}
	
	$query = 'INSERT INTO '.$type.' (id_human) VALUES (\''.$id.'\');';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
}

$query = 'SELECT * FROM '.$type.' GROUP BY id HAVING id_human = \''.$id.'\'';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$id_1 = 0;
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	if ($id_1 != 0)
		break;
	$i = 0;
	foreach ($line as $col_value)
	{
		if ($col_value != null && $i == 0)
		{
			$id_1 = $col_value;
			break;
		}
		$i++;
	}
}

$query = 'INSERT INTO users_'.$type.' (id_'.$type.', name, password) VALUES (\''.$id_1.'\', \''.$login.'\', \''.$pas.'\');';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$query = 'SELECT * FROM users_'.$type.' GROUP BY id HAVING name = \''.$login.'\' AND password = \''.$pas.'\'';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$id_2 = 0;
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	if ($id_2 != 0)
		break;
	$i = 0;
	foreach ($line as $col_value)
	{
		if ($col_value != null && $i == 1)
		{
			$id_2 = $col_value;
			break;
		}
		$i++;
	}
}
pg_close($basisData);
if ($id_2 == 0) printExit("неверый логин или пароль");
else printExit("id = ".$id_2.' ');
?>
