<?php
function printExit($d = '')
{
	print '<p class="boxT0">'.$d.'</p>'; exit;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$name_client = $_POST['name_client'];
	$phone_client = $_POST['phone_client'];
	$id_worker = $_POST['id_worker'];
	$date = $_POST['date'];
	$id_object = $_POST['id_object'];
	$newObject = $_POST['newObject'];
	$price = $_POST['price'];
	$clientd_words = $_POST['clients_words'];

	if ($id_object == 0 && $newObject == "") printExit("неуказан обект");
	if ($name_client == "") printExit("неуказана имя");
	if ($phone_client == "") printExit("неуказан телефон");
	if ($date == "") printExit("неуказана даты");
	if ($id_worker == "") printExit("id_worker");
	
	$failure = '-';
	if ($clientd_words == "") $clientd_words = '-';
	if ($price == "") $price = '-';
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData) printExit("<p>Произошла ошибка соединения: ".pg_last_error());

if ($id_object == 0)
{
	$query = 'INSERT INTO object (name) VALUES (\''.$newObject.'\')';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	$query = 'SELECT id FROM object WHERE object.name = \''.$newObject.'\'';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
	{ foreach ($line as $col_value) $id_object = $col_value; }
}

$id_human = 0;
$query = 'SELECT id FROM human WHERE name = \''.$name_client.'\' AND phone_number = \''.$phone_client.'\'';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{ foreach ($line as $col_value) $id_human = $col_value; }
if ($id_human == 0)
{
	$query = "INSERT INTO human (name, phone_number) VALUES ('".$name_client."', '".$phone_client."')";
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	$query = 'SELECT id FROM human WHERE name = \''.$name_client.'\' AND phone_number = \''.$phone_client.'\'';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
	{ foreach ($line as $col_value) $id_human = $col_value; }
}

$id_client = 0;
$query = 'SELECT id FROM client WHERE id_human = '.$id_human;
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{ foreach ($line as $col_value) $id_client = $col_value; }
if ($id_client == 0)
{
	$query = 'INSERT INTO client (id_human) VALUES ('.$id_human.')';
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	$query = 'SELECT id FROM client WHERE id_human = '.$id_human;
	$result = pg_query($basisData, $query);
	if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
	while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
	{ foreach ($line as $col_value) $id_client = $col_value; }
}

$query = 'INSERT INTO _order (id_client, id_worker, date, failure, id_object, price, clients_words, status, id_order) VALUES ('.
	$id_client.', '.$id_worker.', \''.$date.'\', \''.$failure.'\', '.$id_object.', \''.$price.'\', \''.$clientd_words.'\', \'новый\', \'0\');';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

$id_order = 0;
$query = 'SELECT id FROM _order WHERE id_order = 0';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{ foreach ($line as $col_value) $id_order = $col_value; }

$query = 'UPDATE _order SET id_order = '.$id_order.' WHERE id = '.$id_order;
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

pg_close($basisData);
?>
