import numpy
from random import *

def generate_set(val, max_val, qnt):
    num_set = []
    curr_val = 0
    for i in range(0, qnt-1, 1):
        next_val = randint(1, max_val)
        while next_val == curr_val:
            next_val = randint(1, max_val)
        num_set.append(abs(next_val - curr_val))
        curr_val = next_val
    return num_set

print("Valor: ")
val = int(input())
print("Quantidade de numeros: ")
qnt = int(input())
print("Valor máximo: ")
max_val = int(input())

num_set = generate_set(val, max_val, qnt)

print(num_set)
