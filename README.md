# Synchronization3
Разработать Windows Forms приложение, которое будет использовать объект-семафор следующим образом.  По нажатию на кнопку "Создать поток" создается новый поток и помещается в первый список, где находятся все созданные потоки. Порядковый номер потока увеличивается на один. При двойном клике на потоке, поток перемещается в список ожидающих потоков, где он будет находиться до тех пор, пока в семафоре не освободится для него место. Как только такое место освободилось, поток перемещается из списка ожидания в список рабочих потоков и приступает к работе. Работа заключается в том, чтобы увеличивать локальный счетчик каждого потока на единицу в секунду. При двойном клике по потоку в списке рабочих потоков – поток прекращает свою работу, удаляется из списка и освобождает место для очередного ожидающего потока. Количество свободных мест задается в счетчике. При изменении счетчика более "старые" потоки покидают список, если произошло уменьшение счетчика, или же добавляются новые "ожидающие" потоки при увеличении значения счетчика. 