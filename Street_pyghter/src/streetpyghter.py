import pygame, os
import config
import menu
from game import Point

if __name__ == "__main__":
    print('loading...')
    pygame.init()
    pygame.mixer.init()
    config.Screen()
    
    screen = pygame.Surface((800, 600), 0, 32)


    menu = menu.MainMenu(screen, Point(50,140))
    menu.mainloop()
    