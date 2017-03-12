import argparse
import json
import codecs

parser = argparse.ArgumentParser(description="Create Training Data For Seq2Seq")
parser.add_argument("-i", "--input", type=str, help="input data name")
parser.add_argument("-o", "--output", type=str, help="output data name")
parser.add_argument("filename", type=str, help="filename")

args = parser.parse_args()

def get_data(data, name):
  names = name.split(":")
  ret = data
  for n in names:
    ret = ret[n]
  return ret

datas = []
print("Read %s" % args.filename)
with codecs.open(args.filename, "r", "utf-8-sig") as f:
  lines = f.readlines()

for line in lines:
  datas.append(json.loads(line))

print("Write input data file")
with open("input_%s.txt" % args.input, "w") as f:
  for data in datas:
    i = get_data(data, args.input)
    f.write(" ".join(str(s) for s in i))
    f.write("\n")

print("Write output data file")
with open("output_%s.txt" % args.output, "w") as f:
  for data in datas:
    o = get_data(data, args.output)
    f.write(" ".join(str(s) for s in o))
    f.write("\n")
