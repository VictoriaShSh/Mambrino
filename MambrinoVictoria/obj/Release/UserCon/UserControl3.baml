   M S B A M L   `   `   `  ���� K  GMambrinoVictoria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MambrinoVictoria.UserConMambrinoVictoria.UserCon  +    %MambrinoVictoria.UserCon.UserControl3   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:MambrinoVictoria.UserCon       � 	Resources   �� Source�� $! ../HojaEstilos/Estilos.xaml?�%    .���  � ColumnDefinitions �� $	��20* ��� $
��600* ��� $
��130* � � RowDefinitions �� $
��200* ��� $��40 �.+��� $��0��$��0�����	#FF003DF9�� $��1��$��0�����	#FF003DF9$��3���� 
�������$��1q�$��1��$��0��$��2���� 
�������$��1q�$��1��$��1��$��2���� 
�������$��1q�$��0��$��0��$��2���� -    ��Name$ lista��$��0��$��1��$��2�����	#FFCCECF1$��1,1,1,1q�$��Arial)�$
��Bold"�$��18(� �� View �� ! �� ColumnHeaderContainerStyle ��  �� 
TargetType" ��.���  �� Property � ��	 �� Value	 �	#FF003DF9��  � ��
	 �������  � ��$	 Center=���  � ��
	 �������  � ��$	 2q�.P��� $	��Nº��
 �� Width$
 60�� �� DisplayMemberBinding �� *[0]+�� $��Fecha Inicio Sintomas��$	
 200�� �� *[1]+�� $��Fecha Diagnostico��$	
 200�� �� *[2]+�� $��Sintomas��$	
 250�� �� *[3]+�� $��Diagnostico��$	
 250�� �� *[4]+�� $��Especialidad��$	
 150�� �� *[5]+�� $��Codificacion��$	
 120�� �� *[6]+�� -    ��Name$ añadir��   BotonSimple#���   $��Añadir��$��1��$��1��$��Right=��� -   $ cancelar��#���   $��Cancelar��$��2��$��1��$��25,3,5,3q�