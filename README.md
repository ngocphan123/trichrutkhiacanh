# trichrutkhiacanh
## Code gồm:
- Trích rút khía cạnh bằng C#
- Code tạo vector bằng python
## Quy trình hoạt động:
### Quá trình trainning:
- Đầu vào là các câu comment lấy từ cơ sở dữ liệu
- Chạy file python để tạo vector các từ cần chú ý các thông số của hàm:
```
model = Word2Vec(train_data, size=50, window=5, min_count=1, workers=4, sg=1)
```
+ train_data: dữ liệu cần train
+ size: vector 50 chiều
+ window: cửa số trượt 5
+ min_count: số lần xuất hiện của từ đó ít nhất là 1
+ workers=4, sg=1: 2 thông số không thay đổi
- Sau khi chạy xong file python sẽ lưu các vector vào bảng cơ sở dữ liệu 
### Quá trình test: (mục đích xem câu đầu vào thuộc khía cạnh nào)
- Đầu vào là một comment:
+ hệ thống sẽ tách thành các câu, các từ, rồi tính support của từng từ với từ core theo công thức tính khoảng cách
+ tính tổng trung bình support của các các từ với từng từ core trong một câu
=> tìm ra support của từng câu với từng khía cạnh
+ Đoạn tính support của cả câu đối với từng từ lõi chính là đoạn code sau:
```
string[] str1 = str2.Split(' ');                                                      
                             foreach (var list in datalist)
                             {
                             double support = 0;
                             for (int w = 0; w < str1.Length; w++)
                             {
                                 string tmp = str1[w].ToString();
                                 var vectorword = db.VectorWords.Where(e => e.word == tmp).ToList();
                                
                                //var arrvecw = vectorword.First();
                                 if (vectorword.Count() > 0)
                                 {
                                     var arrvecw = vectorword.First();
                                     string[] arrvecword = arrvecw.vector.Split(',');
                                     double kc = 0;
                                    
                                         string[] arrvec = list.vector.Split(',');
                                         for (int k = 0; k < arrvec.Length; k++)
                                         {
                                             kc += (Convert.ToDouble(arrvec[k]) - Convert.ToDouble(arrvecword[k])) * (Convert.ToDouble(arrvec[k]) - Convert.ToDouble(arrvecword[k]));
                                         }
                                         support += 1/(Math.Sqrt(kc)+ 0.1);
                                      
                                 }
                               }
                             dem++;
                             support = Math.Round (support / (str1.Length), 1);
                             ListSupport sp = new ListSupport(str2,list.idaspect,support);

                             dicSupport.Add(dem, sp);

                             DataRow row = dt.NewRow();
                             row["Col1"] = str2;
                             row["Col2"] = list.idaspect;
                             row["Col3"] = support;
                             row["core"] = list.word;
                             dt.Rows.Add(row);
                             }  
    ```
