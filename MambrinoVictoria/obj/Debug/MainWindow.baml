   M S B A M L   `   `   `  ����K  GMambrinoVictoria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:MambrinoVictoriaMambrinoVictoria  5   	   !    MambrinoVictoria.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MambrinoVictoria     5   	   -   5        $	��800��5   	   $
��1400��6     � 	Resources  5
       �� Source�� 5   
   $ \HojaEstilos\Estilos.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��150* �6   �� 5      $
��100* �6   �� 5      $	��20* �6   �� 5      $	��50* �6   �� 5      $	��20* �6   �� 5      $	��50* �6   �� 5      $	��20* �6   �� 5      $	��50* �6   �� 5      $	��20* �6   �� 5      $	��50* �6   �� 5      $	��20* �6   �� 5      $
��200* �6    � ColumnDefinitions 5   
   �� 5       $
��340* �6   �� 5!      $
��200* �6   �� 5"      $
��300* �6   �� 5#      $
��340* �6   ��5&   
   �� 5'       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5(      �  � Color$ 	#FF97D4E4��6   � 5)      $ 	#FF0D5163��6    � Offset$ 1Q�61   5*      .+�5-   
   ��    Titulo1#���   6   $��Bienvenido a Mambirino��62   $��0��6S   $��4��6c   $��Center=�6w   �� 5/   
    
 Texto1#���  6   $��Usuario:��61   $��1��6D   $��3��6T   �� 50   
   #���  6   $��Puerto:��61   $��1��6C   $��5��6S   �� 51   
   #���  6   $��	Servidor:��61   $��1��6E   $��7��6U   �� 52   
   #���  6   $��Contraseña:��61   $��1��6G   $��9��6W   �� 54   
   -   5         ��Name$ Usuario��54   9     CuadroTexto2#���  6   $��2��6J   $��3��6Z   $��
Arial Bold)�6g   $��22(�6   �� 55   
   -   5        $ Puerto��55   9   #���  6   $��2��6I   $��5��6Y   $��
Arial Bold)�6f   $��22(�6~   �� 56   
   -   5        $ Servidor��56   9   #���  6   $��2��6K   $��7��6[   $��
Arial Bold)�6h   $��22(�6�   G� 57   
   -   5        	 G�Name$	 Contraseña��57      $��Center=�6*   $��Stretch=�6L   $��Stretch=�6j   $
��Left=�6�   $��5,3,5,3q�6�   $��
Arial Bold)�6�   $��25(�6�   $��2��6�   $��9��6�   �� 59   
   -   5        
 ��Name$
 Conectar��59        BotonBasico#���  6#   $��Conectar��6I   $��1��6\   $��11��6l   $��2��6z   6�   �� 5;   
   $��\Resources\enfermera.png��6   $��3��62   $��2��6B   $��9��6O   �� 5<   
   $��\Resources\hospital.png��6   $��0��61   $��2��6A   $��9��6N   5>      5?      