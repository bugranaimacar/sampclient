<?php

    include "header.php";
	$query = "select count(1) FROM serials";		
	$result = mysqli_query($conn, $query);

	if(mysqli_num_rows($result) > 0){

    echo mysqli_num_rows($result);
	}


?>