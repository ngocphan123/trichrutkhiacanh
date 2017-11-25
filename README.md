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
