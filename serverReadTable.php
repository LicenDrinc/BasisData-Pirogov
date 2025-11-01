<?php
function printExit($d = '')
{
	print $d; exit;
}

if ($_SERVER['REQUEST_METHOD'] === 'POST')
{
	$table = $_POST['type_table'];
	
	if ($table == '_order')
	{
		$name_client = $_POST['name_clientPlus'];
		$phone_client = $_POST['phone_clientPlus'];
		$name_worder = $_POST['name_worderPlus'];
		$phone_worder = $_POST['phone_worderPlus'];
		$date = $_POST['datePlus'];
		$failure = $_POST['failurePlus'];
		$id_object = $_POST['id_objectPlus'];
		$price = $_POST['pricePlus'];
		$clientd_woeds = $_POST['clientd_woedsPlus'];
		$status = $_POST['statusPlus'];
		$order = $_POST['id_orderPlus'];
	}
	else if ($table == 'object')
	{
		$name = $_POST['namePlus'];
	}
	else if ($table == 'client')
	{
		$name_client = $_POST['name_clientPlus'];
		$phone_client = $_POST['phone_clientPlus'];
	}
	else if ($table == 'worker')
	{
		$name_worder = $_POST['name_worderPlus'];
		$phone_worder = $_POST['phone_worderPlus'];
	}
	else if ($table == 'human')
	{
		$phone_number = $_POST['phone_numberPlus'];
		$name = $_POST['namePlus'];
	}
}
else
	printExit("нет POST");

$basisData = pg_connect('host=localhost dbname=PCLD user=postgres password=123');
if (!$basisData)
	printExit("<p>Произошла ошибка соединения: ".pg_last_error());

if ($table == '_order' || $table == 'client')
{
	if ($name_client != $phone_client)
		printExit("<p>Имя и номер телефона не совподает ");
}
if ($table == '_order' || $table == 'worker')
{
	if ($name_worder != $phone_worder)
		printExit("<p>Имя и номер телефона не совподает ");
}

$query = 'INSERT INTO '.$table.(
	$table == '_order' ? ' (id_client, id_worker, date, failure, id_object, price, clients_words, status, id_order)' : (
	$table == 'object' ? ' (name)' : (
	$table == 'client' ? ' (id_human)' : (
	$table == 'worker' ? ' (id_human)' : (
	$table == 'human' ? ' (name, phone_number)' : ''
	))))).' VALUES ('.(
	$table == '_order' ? $name_client.', '.$name_worder.', '.$date
		.", '".$failure."', ".$id_object.', '.$price.", '".$clients_words."', '".$status."', ".$order."" : (
	$table == 'object' ? "'".$name."'" : (
	$table == 'client' ? $name_client : (
	$table == 'worker' ? $name_worder : (
	$table == 'human' ? "'".$name."', '".$phone_number."'" : ' 1 '
	)))))
	.');';

$result = pg_query($basisData, $query);
if (!$result)
	printExit("<p>Ошибка запроса: <p>".pg_last_error()."<p>".$query);

pg_close($con);
?>
