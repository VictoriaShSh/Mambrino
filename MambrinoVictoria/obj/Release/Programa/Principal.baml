   M S B A M L   `   `   `  ���� K  GMambrinoVictoria, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:MambrinoVictoria.ProgramaMambrinoVictoria.Programa  C&clr-namespace:MambrinoVictoria.UserConMambrinoVictoria.UserCon  )    #MambrinoVictoria.Programa.Principal   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9local'clr-namespace:MambrinoVictoria.Programa     6uc2&clr-namespace:MambrinoVictoria.UserCon     -   $	��800��$
��1400��  � 	Resources   �� Source�� $ /HojaEstilos/Estilos.xaml?�%    .���  � RowDefinitions �� $��30 ��� $	��50* ��� $	��50* ��� $��30 ��� $��60 ��� $��40 ��� $
��350* ��� $��30 ��� $��60 ��� $��30 ��� $
��150* ��� $��50 � � ColumnDefinitions �� $	��53* ��� $
��500* ��� $
��350* ��� $
��350* ��� $	��53* �.+��� $��0��$��0�����	#FF003DF9$��5���� $��0��$��0�����	#FF003DF9$��12���� $��0��$��4�����	#FF003DF9$��12���� $��1��$��1�����	#FFF7AC33$��3��$��2���� $��3��$��1�����	#FF003DF9$��3���� $��4��$��1��
�������$��2���� $��4��$��3������  �� EndPoint$	 1,0&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FF003DF9�� � Offset$ 1Q�� $ White��$ 0Q��� $��5��$��1�����	#FF003DF9$��3��$��3���� $��8��$��1��
�������$��2���� $��8��$��3������ $	 1,0&�$	 0,0&�.��� $ 	#FF003DF9��$ 1Q�� $ White��$ 0Q��� $��9��$��1�����	#FF003DF9$��3��$��3��o� $��1��$��11��$��2��
������ $��Bottom=�$
��Left=�.@�m� -    m�Name$ inicio��$��Cerrar Sesión��
���   �$��Arial)�$��30(�	 � ToolTip$	 Opciones de Administrador��$
��Bold"�m� -   $ admin��$��Administrador��
���   �$��Arial)�$��30(�$	 Opciones de Administrador��$
��Bold"�$y�	Collapsed=����� 
 �� 
TargetType"
 m�.���  �� Property � �� �� Value �� .R�$�  $� Orientation$ Vertical=�.@�m� -   $ restablecer��$��Restablecer Base de Datos��
���   �$��Arial)�$��20(�$��Medium"�m� -   $ 	modificar��$��Modificar Administrador��
���   �$��Arial)�$��20(�$��Medium"�m� -   $ eliminar��$��Eliminar Usuarios��
���   �$��Arial)�$��20(�$��Medium"�m� -   $ 	consultar��$��Consultar Usuarios��
���   �$��Arial)�$��20(�$��Medium"��� -    ��Name$ 
hospitales��   TextoBlanco#���   $��Seleccione un Hospital:��$��1��$��5��$��1,1,1,1q��� -	   $ pl��#���   $��Seleccione la planta: ��$��2��$��5��$��80,3,5,3q�$y�	Collapsed=��� -
    ��Name$ hospital��  ComboBoxStyle1#���  $��1��$��5�� �� SelectedIndex$ 9��$��	230,3,5,3q��� -   $ planta��#���  $��2��$��5��$��	300,3,5,3q�$y�	Collapsed=��� -   $ nombre_completo��  
TextoNegro#���  $��1��$��1��$��10,0,0,0q��� -   $
 edad��#���  $��2��$��1��$��10,0,0,0q��� -   $
 cama��#���  $��3��$��1��$��	100,0,0,0q��� -   $ sintomas��#���  $��1��$��2��$��10,0,0,0q��� -   $ alerta��#���  $��2��$��2��$��80,0,0,0q��� -   $ diagnostico��#���  $��3��$��2��$��10,0,0,0q��� -    ��Name$ buscar��$��1��$
��Left=�$��3,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Buscar Paciente��.���� $��/Resources/lupa.png��$	 Buscar paciente���� -   $ pacienteNuevo��$��1��$
��Left=�$��63,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Crear Paciente��.���� $"��/Resources/pacienteNuevo.png��$	 Crear Paciente Nuevo���� -   $ asignarCama��$��1��$
��Left=�$��	123,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Hospitalización��.���� $��/Resources/camaMas.png��$	 Hospitalizar Paciente���� -   $ candadoVerde��$��1��$
��Left=�$��	183,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Desbloquear Cama��.���� $!��/Resources/candadoVerde.png��$��47��$	 Desbloquear Cama���� -   $ candadoRojo��$��1��$
��Left=�$��	243,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Bloquear Cama��.���� $ ��/Resources/candadoRojo.png��$	 Bloquear Cama���� -   $ alertas��$��1��$
��Left=�$��	303,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Crear Alerta��.���� $��/Resources/alerta.png��$	 Alerta���� -   $ informes��$��1��$
��Left=�$��	363,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Informes��.���� $��/Resources/informe.png��$	 Lista informes���� -   $
 alta��$��1��$
��Left=�$��	423,0,0,0q�$��4��$��Center=�$��56��$��56��
������ $	 Alta Paciente��.���� $!��/Resources/altaPaciente.png��$	 Alta Hospitalaria���� -   $ notas��$��1��$
��Left=�$��3,0,0,0q�$��8��$��Center=�$��56��$��56��
������ $	 Notas de enfermería��$y�	Collapsed=�.���� $��/Resources/notas.png��$	 Notas de Enfermeria���� -   $ prescripcion��$��1��$
��Left=�$��63,0,0,0q�$��8��$��Center=�$��56��$��56��
������ $	 Hoja de prescripción��$y�	Collapsed=�.���� $!��/Resources/prescripcion.png��$	 Hoja de prescripción���� -   $ 	patologia��$��1��$
��Left=�$��	123,0,0,0q�$��8��$��Center=�$��56��$��56��
������ $&	  Patología/Alteración/Trastorno��$y�	Collapsed=�.���� $��/Resources/patologia.png��$&	  Patología/alteración/trastorno���� $��Disabled=�$
��Auto=�$��1��$��2��$��6��$��3,3,3,3q�$��2��.��� -    �Name$ botones��$��1��$��2��$��6��$��Stretch=�$��Stretch=��� -    ��Name$ camaMini��$��3��$��1��$��30��$
��Left=�$��Center=�$��/Resources/camaVacia.png��$y�	Collapsed=�$��5,3,5,3q��� -   $ 
alertaMini��$��2��$��2��$��30��$
��Left=�$��Center=�$��/Resources/alerta.png��$��5,3,5,3q�$y�	Collapsed=��� -    $ procesos��$��Procesos��$
��Left=�$��Center=�$��27��$��1��$��7��$��86��
����������	#FF0F2F90$
��Bold"�$��Arial)�$��16(�+   %MambrinoVictoria.UserCon.UserControl2  -!     Name$
 usC2��$��1��$��9��$��3��$��2��$y�	Collapsed=�{�.  +   %MambrinoVictoria.UserCon.UserControl3  -"     Name$
 usC3��$��1��$��9��$��3��$��2��$y�	Collapsed=�{�.  