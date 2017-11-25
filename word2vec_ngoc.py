# -*- coding: utf-8 -*-
"""
Created on Sat Nov 04 00:52:23 2017

@author: ngocd
"""
import pandas as pd 
df = pd.read_csv("./testit.txt",sep="/", names=["row"]).dropna()
df.head(10)
print df
import re

def transform_row(row):
    # Xóa số dòng ở đầu câu
    row = re.sub(r"^[0-9\.]+", "", row)
    
    # Xóa dấu chấm, phẩy, hỏi ở cuối câu
    row = re.sub(r"[\.,\?]+$", "", row)
    
    # Xóa tất cả dấu chấm, phẩy, chấm phẩy, chấm thang, ... trong câu
    row = row.replace(",", " ").replace(".", " ") \
        .replace(";", " ").replace("“", " ") \
        .replace(":", " ").replace("”", " ") \
        .replace('"', " ").replace("'", " ") \
        .replace("!", " ").replace("?", " ")
    
    row = row.strip()
    return row 

df["row"] = df.row.apply(transform_row)
#df.head(10)
print("Toi uu hoa xau ky tu")
print(df)
# tach tu 1gram -2gram
from nltk import ngrams

def kieu_ngram(string, n=1):
    gram_str = list(ngrams(string.split(), n))
    return [ " ".join(gram).lower() for gram in gram_str ]

df["1gram"] = df.row.apply(lambda t: kieu_ngram(t, 1))
df["2gram"] = df.row.apply(lambda t: kieu_ngram(t, 2))

df.head(10)
print("Tach tu:")
print(df)

# Combine data và word2vec với Gensim Python
df["context"] = df["1gram"] + df["2gram"]
train_data = df.context.tolist()
print("So luong cau:")
print (len(train_data))

# Training gensim model
from gensim.models import Word2Vec
import logging
  
logging.basicConfig(format='%(asctime)s : %(levelname)s : %(message)s', level=logging.INFO) 
model = Word2Vec(train_data, size=100, window=5, min_count=1, workers=4, sg=1)

print("Mời bạn nhập từ tiếng việt để thực nghiệm:")
s=raw_input()
print("Bạn nhập từ:",s)
print("Gia trị tuong tu của từ:"+s)
print(model.wv.similar_by_word(s))