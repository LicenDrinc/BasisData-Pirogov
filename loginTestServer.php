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
	
	if ($cop_pas != $pas) printExit("Пароль несовподает");
	
	if (stripos($Fname,'\'') !== false || 
	stripos($Lname,'\'') !== false || stripos($Pname,'\'') !== false || 
	stripos($phone,'\'') !== false || stripos($login,'\'') !== false || 
	stripos($pas,'\'') !== false || stripos($cop_pas,'\'') !== false)
		printExit("Неверый данные");
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$query = 'SELECT * FROM users_'.$type.' GROUP BY id HAVING name = \''.$login.'\'';

$result = pg_query($basisData, $query);
if (!$result)
	printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
	printExit("Уже есть человек с таким логином");

pg_close($basisData);
printExit("0");
?>
