<?php
  
  include "baglanti.php";
  $sql = "SELECT SUM(`Username`) AS total_working_hours FROM `players`";
  $result = mysqli_query($conn, $sql);
  $row = mysqli_fetch_object($result) ;
  echo "" . $row->total_working_hours;
  mysqli_close($conn);
?>