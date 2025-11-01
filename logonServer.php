<?php
function printExit($d = '')
{
	print $d; exit;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$type = $_POST['type'];
	$login = $_POST['login'];
	$pas = $_POST['password'];
	
	if ($login == '')
		printExit("логин не указан");
	if ($pas == '')
		printExit("пароль не указан");
	
	if (stripos($pas,'\'') !== false)
		printExit("неверый логин или пароль");
	if (stripos($login,'\'') !== false)
		printExit("неверый логин или пароль");
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$query = 'SELECT * FROM users_'.$type.' GROUP BY id HAVING name = \''.$login.'\' AND password = \''.$pas.'\'';

$result = pg_query($basisData, $query);
if (!$result)
	printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$id = 0;
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{
	if ($id != 0)
		break;
	$i = 0;
	foreach ($line as $col_value)
	{
		if ($col_value != null && $i == 1)
		{
			$id = $col_value;
			break;
		}
		$i++;
	}
}
pg_close($con);
if ($id == 0)
	printExit("неверый логин или пароль");
else
	printExit("id = ".$id.' ');
?>
