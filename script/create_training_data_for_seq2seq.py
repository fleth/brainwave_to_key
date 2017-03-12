import argparse
import json

parser = argparse.ArgumentParser(description='Create Training Data For Seq2Seq')
parser.add_argument('-i', '--input', type=str, help="input data name")
parser.add_argument('-o', '--output', type=str, help="output data name")
parser.add_argument('filename', type=str, help="filename")

args = parser.parse_args()

print("Read %s" % args.filename)
with open(args.filename) as f:
  datas = json.load(f)

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
