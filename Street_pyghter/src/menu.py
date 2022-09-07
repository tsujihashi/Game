import pygame, os, sys
from pygame.locals import *
from random import randint
from game import Point
import config
import game

res_dir = os.path.dirname(__file__).replace('src', 'res')
sys.path.append(res_dir)


def getBckgrndList():
    folder = f'{res_dir}/Background'
    list = os.listdir(folder)
    list2 = os.listdir(folder)
    for file in list2:
        if os.path.isdir(os.path.join(folder ,file)) or file.find('Bckgrnd') < 0:
            list.remove(file)
    return list

class YesNo:
    def __init__(self, string, value = True, position = Point(0,0)):
        self.yes = Text(string+' : Yes', position).sprite
        self.no = Text(string+' : No', position).sprite
        self.choice = value
    
    def more(self):
        self.switch()
        
    def less(self):
        self.switch()
    
    def switch(self):
        self.choice = not self.choice
    
    def print_me(self, screen):
        if self.choice:
            screen.blit(self.yes, self.position.value())
        else:
            screen.blit(self.no, self.position.value())


class Text:
    def __init__(self, string, position = Point(0,0)):
        self.string = string
        self.position = position
        self.letters = config.Alphabet().sprites
        self.sprite = self.convert()
        
    def convert(self):
        assert(isinstance(self.string,str))
        length = len(self.string)
        sprite = pygame.Surface((15*(length+1), 32)).convert_alpha()
        sprite.fill((0,0,0,0))
        for index in range(length):
            num = ord(self.string[index])
            line = num//16
            column = num-(line*16)
            letter = self.letters[line][column]
            if letter != None:
                sprite.blit(letter, (index*15, 0))
        return sprite
    
    def print_me(self, screen, position = Point(0,0)):
        screen.blit(self.sprite, (self.position+position).value())
            
class Menu:
    def __init__(self, position, screen, background = 'MenuScreen.png'):
        self.sprites = config.Alphabet().sprites
#        self.sprites = SpriteSheetLoader(f'{res_dir}/Ascii.png', 16, 16).getSpriteList()
        self.cursor = pygame.image.load(f'{res_dir}/cursor.png').convert_alpha()
        self.screen = screen
        self.options = []
        self.position = position
        self.choice = 0
        self.background = background
    
    def addElt(self, elt):
        if isinstance(elt, Text):
            position = self.position + (0, 3+len(self.options)*16)
        else:
            position = self.position + (15, 3+len(self.options)*16)
        elt.position = position
        self.options.append(elt)
    
    def back(self):
        return 0
        
    def mainloop(self):
        background = pygame.image.load(f'{res_dir}/Background/'+self.background).convert()
        while True:
        
            ## Conditions d'arret du programme
            for event in pygame.event.get():
                if event.type == QUIT:
                    exit()
                if event.type == KEYDOWN:
                    if event.key == K_ESCAPE:
                        config.SoundPlayer().play_sound('menucancel.wav')
                        return self.back()
                    if event.key == K_UP:
                        config.SoundPlayer().play_sound('menumove.wav')
                        self.up()
                    if event.key == K_DOWN:
                        config.SoundPlayer().play_sound('menumove.wav')
                        self.down()
                    if event.key == K_RIGHT or event.key == K_RETURN:
                        config.SoundPlayer().play_sound('menuok.wav')
                        self.more()
                    if event.key == K_LEFT:
                        config.SoundPlayer().play_sound('menuok.wav')
                        self.less()
            
            ## Rafraichissement de l'ecran
            self.screen.fill((0,0,0))
            ## BG
            self.screen.blit(background, (0,0))
            
            ## Affiche le GameObject
            self.print_me()
            
            config.Screen().display_update(self.screen)
            
    def up(self):
        self.choice -= 1
        if self.choice < 0:
            self.choice = 0
        if isinstance(self.options[self.choice], Text):
            if self.choice == 0:
                self.down()
            else:
                self.up()
    
    def down(self):
        self.choice += 1
        if self.choice >= len(self.options):
            self.choice = len(self.options)-1
        if isinstance(self.options[self.choice], Text):
            if self.choice == len(self.options)-1:
                self.up()
            else:
                self.down()

    def more(self):
        option = self.options[self.choice]
        option.more()
    
    def less(self):
        self.options[self.choice].less()

    def print_me(self):
        cursor_pos = self.position + (3, self.choice*16)
        self.screen.blit(self.cursor, cursor_pos.value())
        for option in self.options:
            option.print_me(self.screen)
    
    def tick_me(self):
        pass

class MenuElt:
    def __init__(self, string, function, position = Point(0,0)):
        self.string = string
        self.position = position
        self.text = Text(string, position)
        self.function = function
    
    def print_me(self, screen):
        if self.position != self.text.position:
            self.text.position = self.position
        self.text.print_me(screen)
    
    def more(self):
        self.function()
    
    def less(self):
        return

class MainMenu(Menu):
    def __init__(self, screen, position = Point(0,0)):
        Menu.__init__(self, position, screen, 'MenuScreen.png')
        self.addElt(MenuElt('Start Vs Game', self.call_game))
    
    def back(self):
        print('quit')
        exit()
    
    def call_game(self):
        print('call_game')
        config.Screen()
        screen = pygame.Surface((800, 600), 0, 32)
        gc = GameScreen(screen, Point(50,140))
        gc.mainloop()

class GameScreen:
    def __init__(self, screen, position = Point(0,0)):
        self.screen = screen
        self.position = position
        self.background = 'OptionScreen.png'
    def back(self):
        print('quit')
        exit()

    def mainloop(self):
        background = pygame.image.load(f'{res_dir}/Background/{self.background}').convert()
        while True:
        
            ## Conditions d'arret du programme
            for event in pygame.event.get():
                if event.type == QUIT:
                    exit()
                if event.type == KEYDOWN:
                    if event.key == K_ESCAPE:
                        config.SoundPlayer().play_sound('menucancel.wav')
                        return self.back()
                    if event.key == K_UP:
                        config.SoundPlayer().play_sound('menumove.wav')
                        self.up()
                    if event.key == K_DOWN:
                        config.SoundPlayer().play_sound('menumove.wav')
                        self.down()
                    if event.key == K_RIGHT:
                        config.SoundPlayer().play_sound('menumove.wav')
                        self.right()
                    if event.key == K_LEFT:
                        config.SoundPlayer().play_sound('menumove.wav')
                        self.left()
            
            ## Rafraichissement de l'ecran
            self.screen.fill((0,0,0))
            ## BG
            self.screen.blit(background, (0,0))
            
            ## Affiche le GameObject
            #self.print_me()
            
            config.Screen().display_update(self.screen)

    def up(self):
        print('up')
    
    def down(self):
        print('down')

    def right(self):
        print('right')
    
    def left(self):
        print('left')
    
    def tick_me(self):
        pass


if __name__ == "__main__":
    pygame.init()
    screen = pygame.display.set_mode((800, 600), 0, 32)
    pygame.display.set_caption("MenuTest") # program title
    menu = MainMenu(screen, Point(20,10))
    menu.mainloop()