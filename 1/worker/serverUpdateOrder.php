<?php
function printExit($d = '')
{
	print '<p class="boxT0">'.$d.'</p>'; exit;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$id = $_POST['id'];
	$date = $_POST['date'];
	$price = $_POST['price'];
	$failure = $_POST['failure'];
	$status = $_POST['status'];

	if ($date == "") printExit("неуказана даты");
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData) printExit("<p>Произошла ошибка соединения: ".pg_last_error());

$id_order = [];
$query1 = 'SELECT * FROM _order WHERE id_order = '.$id.' ORDER BY id';
$query = $query1;
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);
while ($line = pg_fetch_array($result, null, PGSQL_ASSOC))
{ $i = 0; foreach ($line as $col_value) { $id_order[$i] = $col_value; $i++; } }

if ($failure == "") $failure = $id_order[4];
if ($price == "") $price = $id_order[6];

$query = 'INSERT INTO _order (id_client, id_worker, date, failure, id_object, price, clients_words, status, id_order) VALUES ('.
	$id_order[1].', '.$id_order[2].', \''.$date.'\', \''.$failure.'\', '.$id_order[5].', \''.$price.'\', \''.$id_order[7].'\', \''.$status.'\', \''.$id.'\');';
$result = pg_query($basisData, $query);
if (!$result) printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

pg_close($basisData);
?>
