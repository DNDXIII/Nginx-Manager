import React from 'react';
import RaisedButton from 'material-ui/RaisedButton';


export default class Terminal extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            text: [],
        };
    }

    onLineSubmit = (event) => {
        event.preventDefault();
        var newText = this.state.text;
        newText.push(document.getElementById("inputfield").value + "\n");
        document.getElementById("inputfield").value = "";
        this.setState({
            text: newText,
        });
        var div = document.getElementById("textBox");
        div.scrollTop = div.scrollHeight - div.clientHeight;
    }

    handleKeyPress = (event) => {
        if (event.keycode == 38) {
            alert('ububu')
        }
    }

    render() {
        return (
            <div>
                <pre id="textBox" style={{
                    fontSize: 17,
                    width: 1200,
                    height: 400,
                    overflow: "auto",
                    backgroundColor: "black",
                    color: "white",
                    fontFamily: "Lucida Console",
                    paddingBottom: "2%",
                    margin: 0
                }}>
                    {this.state.text}
                </pre>
                <form autoComplete="off" onSubmit={this.onLineSubmit}>
                    <input type="text" onKeyDown={this.handleKeyPress} id="inputfield" style={{
                        width: 1200,
                        border: "none",
                        borderColor: "transparent",
                        backgroundColor: "black",
                        color: "white",
                        fontFamily: "Lucida Console",
                        fontSize: 17
                    }} />

                    <button type="submit" style={{ display: "none" }} />
                </form>
                <RaisedButton onTouchTap={this.props.handleDisconnect} style={{ margin: 13, marginBottom: 20 }} label="Disconnect" />
            </div>
        );
    }
}