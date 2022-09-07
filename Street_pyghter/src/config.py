import pygame, sys, os
from pygame.locals import *
from game import SpriteSheetLoader

res_dir = os.path.dirname(__file__).replace('src', 'res')
sys.path.append(res_dir)

class  Screen(object):
    instance = None       # Attribut statique de classe
    def __new__(cls): 
        "méthode de construction standard en Python"
        if cls.instance is None:
            cls.instance = object.__new__(cls)
            cls.init(cls.instance)
        return cls.instance
    
    def init(self):
        print("initialising screen...")
        pygame.display.set_caption("StreetPyghter")
        self.screen = pygame.display.set_mode((800, 600), 0, 32)
        
    def display_update(self, screen):
        self.screen.blit(screen, (0,0))
        pygame.display.update()

class  Alphabet(object):
    instance = None       # Attribut statique de classe
    def __new__(cls): 
        "méthode de construction standard en Python"
        if cls.instance is None:
            cls.instance = object.__new__(cls)
            cls.sprites = SpriteSheetLoader(f'{res_dir}/Ascii.png', 16, 16, True).getSpriteList()
        return cls.instance

class SoundPlayer:
    instance = None       # Attribut statique de classe
    def __new__(cls): 
        "méthode de construction standard en Python"
        if cls.instance is None:
            print('creating SoundPlayer')
            cls.instance = object.__new__(cls)
            cls.sound_vol = 1
        return cls.instance
    
    def play_sound(self, file):
        if file.find(f'{res_dir}/sound/') < 0:
            file = f'{res_dir}/sound/'+file
        sound = pygame.mixer.Sound(file)
        sound.set_volume(self.sound_vol)
        sound.play()
            
if __name__ == "__main__":
    print('done')