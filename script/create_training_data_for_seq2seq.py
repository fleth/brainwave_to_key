import argparse
import json
import codecs

parser = argparse.ArgumentParser(description='Create Training Data For Seq2Seq')
parser.add_argument('-i', '--input', type=str, help="input data name")
parser.add_argument('-o', '--output', type=str, help="output data name")
parser.add_argument('filename', type=str, help="filename")

args = parser.parse_args()

datas = []
print("Read %s" % args.filename)
with codecs.open(args.filename, "r", 'utf-8-sig') as f:
  lines = f.readlines()

for line in lines:
  datas.append(json.loads(line))

print("Write input data file")
with open("input_%s.txt" % args.input) as f:
  for data in datas:
    i = data[args.input]
    f.writeline(" ".join(i))

print("Write output data file")
with open("output_%s.txt" % args.output) as f:
  for data in datas:
    o = data[args.output]
    f.writeline(" ".join(i))
