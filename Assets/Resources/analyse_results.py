import matplotlib.pyplot as plt
import json

analytics_file_path="/Users/nikhilbhat/Library/Application Support/DefaultCompany/mobilegameproject/analytics.json";
analytics_json={}

with open(analytics_file_path,"r") as input_file:
    analytics_json=json.load(input_file)

print (analytics_json)

labels = ["Answered Correct","Answered Wrong"]
sizes = [analytics_json["success_tries"],analytics_json["failure_tries"]]
plt.pie(sizes, labels=labels,
autopct='%1.1f%%', shadow=True)
plt.axis('equal')
plt.title("Correct vs Wrong")
plt.show()

labels=[]
sizes=[]
for theme in analytics_json["themeWise"]:
    labels.append(theme["name"])
    sizes.append(theme["count"])
plt.pie(sizes, labels=labels,
autopct='%1.1f%%', shadow=True)
plt.axis('equal')
plt.title("Theme Analysis")
plt.show()

labels=[]
sizes=[]
for level in analytics_json["levelWise"]:
    labels.append(level["name"])
    sizes.append(level["count"])
plt.pie(sizes, labels=labels,
autopct='%1.1f%%', shadow=True)
plt.axis('equal')
plt.title("Level Analysis")
plt.show()


labels = ["Freeze Time","Reveal Character","Delete Character from Keyboard"]
sizes = [analytics_json["freeze_time_count"],analytics_json["reveal_character_count"],analytics_json["delete_character_count"]]
plt.pie(sizes, labels=labels,
autopct='%1.1f%%', shadow=True)
plt.axis('equal')
plt.title("Sinks Analysis")
plt.show()


x = ['Success with Sink', 'Failure with Sink']
values = [analytics_json["sink_and_success_count"],analytics_json["sink_and_fail_count"]]
x_pos = [i for i, _ in enumerate(x)]
plt.bar(x_pos, values, color='green')
plt.xlabel("Results")
plt.ylabel("Counts")
plt.title("Correct vs Wrong after using Sinks")
plt.xticks(x_pos, x)
plt.show()

x = ['Total Tries', 'Success','Failure',"Quit"]
values = [analytics_json["all_tries"],analytics_json["success_tries"],analytics_json["failure_tries"],analytics_json["all_tries"]-analytics_json["success_tries"]-analytics_json["failure_tries"]]
x_pos = [i for i, _ in enumerate(x)]
plt.bar(x_pos, values, color='green')
plt.xlabel("Results")
plt.ylabel("Counts")
plt.title("Success vs Failure vs Quits")
plt.xticks(x_pos, x)
plt.show()

x = ['Average Questions Per Play', 'Plays during first time']
values = [analytics_json["all_tries"]/analytics_json["game_opened_count"],analytics_json["first_time_tries"]]
x_pos = [i for i, _ in enumerate(x)]
plt.bar(x_pos, values, color='green')
plt.xlabel("Results")
plt.ylabel("Counts")
plt.title("Plays over time")
plt.xticks(x_pos, x)
plt.show()

