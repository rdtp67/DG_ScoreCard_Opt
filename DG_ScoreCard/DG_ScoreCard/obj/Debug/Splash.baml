   M S B A M L   `   `   `  ����  DG_ScoreCard    DG_ScoreCard.Window1   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$  Login��5   	   $	��600��6   $	��900��6$    � Icon$  Media/pacman_like_icon.png��60    � WindowStyle$
 None=�6R    � WindowStartupLocation$ CenterScreen=�6e   ��6�   Z�  � 	Resources 5         ButtonControlTemplate1 �� 
TargetType � 	Resources  Key �� TargetProperty �� 
TargetName8  2System.Windows.Media.Animation.EasingColorKeyFrame	 �� KeyTime
 �� Value ��Name �� RadiusX �� RadiusY F�Name �� Stretch J� RenderTransformOrigin �� EndPoint �� 
StartPoint � Color � Offset �� Angle T� X T� Y �� Triggers P� Property P� Value N� EnterActions  Storyboard1 �� Property �� Value �� 
TargetName  FocusVisual J� SnapsToDevicePixels  �� StrokeDashArray  Button.Static.Background! �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background 	 Button.Pressed.Border 
 Button.Disabled.Background  Button.Disabled.Border  Button.Disabled.Foreground  minimizeButton" �� 
TargetType# J� Opacity  resizeButton  
exitButton��%   &	        7  &	 5    &	 H    &	 �    &	     &	 8    &	 [    &	 ~    &		 �    &	
 �    &	 �    &	 
    &	 -    7  7  7  &	 
    7  &	 �    7  7  7  �� 5   
   " ��69    5      ���� 5	      Storyboard1 .��5
      �� $, &(Shape.Stroke).(SolidColorBrush.Color)�63   $ 	rectangle��6v   .g�5        $	 0��6.   $
 White��6:     5      $	 0:0:1��6.   $
 	#FFE29D9D��6>     5      $	 0:0:2��6.   $
 White��6>   5      �� 5      $* $(Shape.Fill).(SolidColorBrush.Color)�63   $ 	rectangle��6t   .g�5        $	 0��6.   $
 	#FFE60000��6:     5      $	 0:0:1��6.   $
 Black��6>     5      $	 0:0:2��6.   $
 	#FFE60000��6>   5      5      5      .R�5      � ��6   Z� .+�5      �� $ 	rectangle��6   
�������$	 9.5��6>   $	 9.5��6L   $��0,0,0,-0.375q�6Z   ���	#FFE600006p   F� 5      $ path1��6   ������� ��� ��;G���	$��Stretch=�6;   $��
15,10,19,9q�6Y   $
 Fill=�6m   
�������$
��Auto��6�   $��2��6�   $ 0.557,0.429&�6�   z�5      V� 5      .~�5      �� �� 5      �� 5      T� 5      5      ��5!      �� 5"      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5#      � $ 	#FFE6CBCB��6+   � 5$      $ White��6+   $ 1Q�69   5%      5'      F� 5(      $
 path��6   ������� ��� ��;G���	$��Stretch=�6:   $��17.25,10.375,16.75,8.625q�6X   $
 Fill=�6z   
�������$
��Auto��6�   $ 0.589,0.619&�6�   $��2��6�   z�5)      V� 5*      .~�5+      �� �� 5,      �� 5-      $ -54.275Q�6.   T� 5.      $
 0.51Q�61   $ -4.003Q�6:   5/      ��51      �� 52      $ 0.5,1&�6.   $ 0.5,0&�6?   .��53      � $ 	#FFE6CBCB��6+   � 54      $ White��6+   $ 1Q�69   55      57      58       59      P� 5:       � �� .  5;      �� 5<      8��  6*   . �5>      ��  � �� �	#FFDDDDDD63   �� 5?      $
 path��6-    � ��
 �   �6?   �� 5@      $
 path��6/    � ��
 �   �6A   �� 5A      $ path1��6/    � ��
 �   �6B   �� 5B      $
 path��68    � z� 5C      V� 5D      .~�5E   "   �� �� 5F   "   �� 5G   "   $ -59.42Q�62   T� 5H   "   $
 0.51Q�65   $ -4.003Q�6>   5I      5K      �� 5L      $ path1��6/    � ��$ 17.75,10.125,17.125,8.5q�6B   �� 5M      $ path1��68    � z� 5N      V� 5O      .~�5P   "   �� �� 5Q   "   �� 5R   "   $ 6.981Q�62   T� 5S   "   5T      5V      �� 5W      $ 	rectangle��6/    � ��$ 0.625,0.125,-0.625,-0.5q�6F   �� 5X      $ 	rectangle��6-    � �� 5Y      �� 5Z      $ 0.5,1&�62   $ 0.5,0&�6C   .��5[   "   � $ White��6/   � 5\   "   $ 	#FFA20606��6/   $ 1Q�6A   � 5]   "   $ 	#FFE2AAAA��6/   $ 0Q�6A   5^      5`      �� 5a      $ 	rectangle��6/    � �� �	#FFAC43436F   5b      P� 5c       �@J�IsMouseOver . . �5d      �� $ 	rectangle��60    �@J�Opacity$ 1Q�6G   �� 5e      $ 	rectangle��6-    � �� 5f      �� 5g      $ 0.5,1&�62   $ 0.5,0&�6C   .��5h   "   � $ White��6/   � 5i   "   $ 	#FFA20606��6/   $ 1Q�6A   5j      5l      5m      5o      �� 5p   
   6   �� 5q   
   .�5r      ��  � �� 5s      �� 5t      .R�5u      �� $��2q�6$    . #��� ��6J   $��1��6�   $	  1 2R�6�   5v      5x      5y      �� 5z   
   $! 	#FFDDDDDD��6;   �� 5{   
   $! 	#FF707070��67   �� 5|   
   $! 	#FFBEE6FD��6>   �� 5}   
   $! 	#FF3C7FB1��6:   �� 5~   
   $! 	#FFC4E5F6��6<   �� 5   
   $! 	#FF2C628B��68   �� 5�   
   $! 	#FFF4F4F4��6=   �� 5�   
   $! 	#FFADB2B5��69   �� 5�   
   $! 	#FF838383��6=   �� 5�   
   "" ��6'   .�5�      ��  �@�FocusVisualStyle8   61   �� 5�       � ��8  6+   �� 5�       � ��8  6,   �� 5�       � ��# � ��6+   �� 5�       � ��$ 1q�60   �� 5�       � ��$ Center=�6;   �� 5�       � ��$ Center=�69   �� 5�       � ��$ 1q�6(   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � ��6   Z� .+�5�      �� $ 	rectangle��6(   ���	#FF0061C1$ 5��6L   $ 5��6X   
�������6d   �� 5�      
�������$��2.875,2.75,2.75,0q�65   $ 5��6P   $ 5��6\   
�������$��0��6w   $	# 0.2Q�6�   $��11.25��6�   $	��Top=�6�   5�       5�      P� 5�       �@��IsDefaulted . 6=   P� 5�       �@J�IsMouseOver . 6=   P� 5�       � �� . 6;   P� 5�       � {� .  6;   P� 5�       �@J�IsMouseOver . . �5�   "   �� $ 	rectangle��69    � �� �	#FFE822226P   5�      5�      5�      5�      �� 5�   
   "" ��6%   .�5�      ��  �@�FocusVisualStyle8   61   �� 5�       � �� 5�      �� 5�      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5�      � $ Black��6'   $ 0Q�65   � 5�      $ 	#FF060C8F��6'   $ 1Q�69   5�      5�      �� 5�       � ��# �  6,   �� 5�       � ��# � ��6+   �� 5�       � ��$ 1q�60   �� 5�       � ��$ Center=�6;   �� 5�       � ��$ Center=�69   �� 5�       � ��$ 1q�6(   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � ��6   Z� .+�5�      �� $ 	rectangle��6(   ���	#FFAA1212$ 5��6L   $ 5��6X   
�������6d   �� 5�      
�������$��2.875,2.75,2.75,0q�65   $ 5��6P   $ 5��6\   
�������$��0��6w   $	# 0.2Q�6�   $��11.25��6�   $	��Top=�6�   5�       5�      P� 5�       �@��IsDefaulted . 6=   P� 5�       �@J�IsMouseOver . 6=   P� 5�       � �� . 6;   P� 5�       � {� .  6;   P� 5�       �@J�IsMouseOver . . �5�   "   �� $ 	rectangle��69    � �� �	#FFE822226P   5�      5�      5�      5�      �� 5�   
   "" ��6#   .�5�      ��  �@�FocusVisualStyle8   61   �� 5�       � ��8  6+   �� 5�       � ��8  6,   �� 5�       � ��# � ��6+   �� 5�       � ��$ 1q�60   �� 5�       � ��$ Center=�6;   �� 5�       � ��$ Center=�69   �� 5�       � ��$ 1q�6(   �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � ��6   Z� .+�5�      �� $ 	rectangle��6(   ���	#FFAA1212$ 5��6L   $ 5��6X   
�������6d   �� 5�      
�������$��2.875,2.75,2.75,0q�65   $ 5��6P   $ 5��6\   
�������$��0��6w   $	# 0.2Q�6�   $��11.25��6�   $	��Top=�6�   5�       5�      P� 5�       �@��IsDefaulted . 6=   P� 5�       �@J�IsMouseOver . 6=   P� 5�       � �� . 6;   P� 5�       � {� .  6;   P� 5�       �@J�IsMouseOver . . �5�   "   �� $ 	rectangle��69    � �� �	#FFE822226P   5�      5�      5�      5�      5�      .��5�      � $	��592��6   $��Bottom=�6   ���	#FF3D1F51$ � RowDefinitions$ 5�   
   �� 5�      $��0* �6   �� 5�      .+�5�   
   �� $
��51.5��6   $��2��6"   ���	#FFCFCFCF$	��Top=�6F   ��5�      �� 5�      $ 0.5,1&�6&   $ 0.5,0&�67   .��5�      � $ 	#FF343434��6#   $ 0.241Q�65   � 5�      $ 	#FFAAAAAA��6#   $ 0.861Q�65   5�      5�      �� 5�   
   -   5        % ��Name$% 
exitButton��5�      $��Button��6%   $
��Left=�66   $��30��6Q   $	��Top=�6]   $��30��6u   $ 0.967,0.283&�6�   $��845.25,15,0,0q�6�   $��2��6�   ��5�      �� 5�      .R�5�      � 5�      ��5�      �� 5�      "" ��6   .�5�      ��  �@�FocusVisualStyle# [ 69   �� 5�       � ��# [ 63   �� 5�       � ��# [ 64   �� 5�       � ��# � ��63   �� 5�       � ��$ 1q�68   �� 5�       � ��$ Center=�6C   �� 5�       � ��$ Center=�6A   �� 5�       � ��$ 1q�60   �� 5�       � �� 5      �� 5     " ��6.   .R�5  "   � ��6'   Z� .+�5  &   �� $ 	rectangle��60   ���	#FFAA1212$ 5��6T   $ 5��6`   
�������6l   �� 5  &   
�������$��2.875,2.75,2.75,0q�6=   $ 5��6X   $ 5��6d   
�������$��0��6   $	# 0.2Q�6�   $��11.25��6�   $	��Top=�6�   5  #    5  "   P� 5  &    �@��IsDefaulted . 6E   P� 5  &    �@J�IsMouseOver . 6E   P� 5	  &    � �� . 6C   P� 5
  &    � {� .  6C   P� 5  &    �@J�IsMouseOver . . �5  *   �� $ 	rectangle��6A    � �� �	#FFE822226X   5  '   5     5     5     5     �� 5  
   
�������$��9��6!   $��
0,51.5,0,0q�6,   $��2��6@   ���	#FFCFCFCF$	��Top=�6d   $	# 0.2Q�6|   $��0��6�   �� 5  
   -   5        $% resizeButton��5     $��Button��6'   $
��Left=�68   $��30��6S   $	��Top=�6_   $��30��6w   $ 0.967,0.283&�6�   $��810.25,15,0,0q�6�   $��2��6�   ��5     �� 5     .R�5     � 5     ��5     �� 5     "" ��6   .�5     ��  �@�FocusVisualStyle# [ 69   �� 5      � ��# [ 63   �� 5       � ��# [ 64   �� 5!      � ��# � ��63   �� 5"      � ��$ 1q�68   �� 5#      � ��$ Center=�6C   �� 5$      � ��$ Center=�6A   �� 5%      � ��$ 1q�60   �� 5&      � �� 5'     �� 5(     " ��6.   .R�5)  "   � ��6'   Z� .+�5*  &   �� $ 	rectangle��60   ���	#FF0061C1$ 5��6T   $ 5��6`   
�������6l   �� 5+  &   $��2.875,2.75,2.75,0q�60   $ 5��6K   $ 5��6W   
�������$��0��6r   $	# 0.2Q�6�   $��11.25��6�   $	��Top=�6�   
�������6�   5,  #    5-  "   P� 5.  &    �@��IsDefaulted . 6E   P� 5/  &    �@J�IsMouseOver . 6E   P� 50  &    � �� . 6C   P� 51  &    � {� .  6C   P� 52  &    �@J�IsMouseOver . . �53  *   �� $ 	rectangle��6A    � �� �	#FF3A8BDC6X   54  '   56     58     59     5;     �� 5<  
   -   5        $% minimizeButton��5<     $��Button��6)   $
��Left=�6:   $��30��6U   $	��Top=�6a   $��30��6y   $ 0.967,0.283&�6�   $��775.25,15,0,0q�6�   $��2��6�   ��5=     �� 5>     .R�5?     � 5@     ��5B     �� 5C     "" ��6   .�5D     ��  �@�FocusVisualStyle# [ 69   �� 5E      � ��# [ 63   �� 5F      � ��# [ 64   �� 5G      � ��# � ��63   �� 5H      � ��$ 1q�68   �� 5I      � ��$ Center=�6C   �� 5J      � ��$ Center=�6A   �� 5K      � ��$ 1q�60   �� 5L      � �� 5M     �� 5N     " ��6.   .R�5O  "   � ��6'   Z� .+�5P  &   �� $ 	rectangle��60   ���	#FF0061C1$ 5��6T   $ 5��6`   
�������6l   �� 5Q  &   $��2.875,2.75,2.75,0q�60   $ 5��6K   $ 5��6W   
�������$��0��6r   $	# 0.2Q�6�   $��11.25��6�   $	��Top=�6�   
�������6�   5R  #    5S  "   P� 5T  &    �@��IsDefaulted . 6E   P� 5U  &    �@J�IsMouseOver . 6E   P� 5V  &    � �� . 6C   P� 5W  &    � {� .  6C   P� 5X  &    �@J�IsMouseOver . . �5Y  *   �� $ 	rectangle��6A    � �� �	#FF3A8BDC6X   5Z  '   5\     5^     5_     5a     �� 5b  
   ���	#FFA41C1C$��Right=�6%   $��0,66.5,10,10q�6A   $��2��6W   ���	#FFCFCFCF$��0��6{   5d     5e     