<!DOCTYPE html>
<html lang="ja">
  <head>
    <meta charset="utf-8">
    <title>わかめ</title>
  </head>
  <body>
    <!-以下受信機能->
     <?php
     $huuu = $_POST["123"];
     //パスワード機能
     if ($huuu == "ジョジョ" || $huuu == "jojo"){
       echo '
       <!DOCTYPE html>
       <html lang="en" dir="ltr">
         <head>
           <meta charset="utf-8">
           <title>人間を辞めました。</title>
         </head>
         <body>
           <iframe width="560" height="315" src="https://www.youtube.com/embed/7UYOkOsOj1M" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
         </body>
       </html>  ';
     }else {
       echo '
       <!DOCTYPE html>
       <html lang="en" dir="ltr">
         <head>
           <meta charset="utf-8">
           <title>error</title>
         </head>
         <body>
           <h1>名前が違います。</h1>
         </body>
       </html>';
     }
      ?>
  </body>
</html>
